import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TarefaService } from '../tarefa.service';
import { Tarefa } from '../tarefa.model';

@Component({
  selector: 'app-editar-tarefa',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './editar-tarefa.component.html',
  styleUrls: ['./editar-tarefa.component.css']
})
export class EditarTarefaComponent implements OnInit {
  tarefaForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private tarefaService: TarefaService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.tarefaForm = this.fb.group({
      id: ['', Validators.required],
      titulo: ['', Validators.required],
      descricao: ['', Validators.required],
      status: ['', Validators.required],
      dataVencimento: ['', Validators.required]
    });
  }

  ngOnInit(): void {

  }

  onSubmit(): void {
    if (this.tarefaForm.valid) {
      const tarefaAtualizada: Tarefa = this.tarefaForm.value;
      this.tarefaService.updateTarefa(tarefaAtualizada.id, tarefaAtualizada).subscribe(() => {
        this.snackBar.open('Tarefa atualizada com sucesso!', 'Fechar', {
          duration: 3000
        });
        this.router.navigate(['/tarefas']);
      }, error => {
        this.snackBar.open('Erro ao atualizar a tarefa.', 'Fechar', {
          duration: 3000
        });
      });
    }
  }
}
