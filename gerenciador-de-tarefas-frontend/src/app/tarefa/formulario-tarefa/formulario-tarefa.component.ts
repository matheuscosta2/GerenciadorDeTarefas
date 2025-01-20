import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TarefaService } from '../tarefa.service';
import { Tarefa } from '../tarefa.model';

@Component({
  selector: 'app-formulario-tarefa',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './formulario-tarefa.component.html',
  styleUrls: ['./formulario-tarefa.component.css']
})
export class FormularioTarefaComponent {
  tarefaForm: FormGroup;

  constructor(private fb: FormBuilder, private tarefaService: TarefaService, private snackBar: MatSnackBar, private router: Router) {
    this.tarefaForm = this.fb.group({
      titulo: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      descricao: [''],
      dataVencimento: ['', [Validators.required, this.dataVencimentoValidator]],
      status: ['', Validators.required]
    });
  }

  dataVencimentoValidator(control: any): { [key: string]: boolean } | null {
    const dataVencimento = new Date(control.value);
    const dataAtual = new Date();
    if (dataVencimento < dataAtual) {
      return { 'dataVencimentoInvalida': true };
    }
    return null;
  }

  onSubmit(): void {
    if (this.tarefaForm.valid) {
      const novaTarefa: Tarefa = this.tarefaForm.value;
      this.tarefaService.addTarefa(novaTarefa).subscribe(() => {
        this.snackBar.open('Tarefa salva com sucesso!', 'Fechar', {
          duration: 3000
        });
        this.router.navigate(['/tarefas']);
      }, error => {
        this.snackBar.open('Erro ao salvar a tarefa.', 'Fechar', {
          duration: 3000
        });
      });
    }
  }
}
