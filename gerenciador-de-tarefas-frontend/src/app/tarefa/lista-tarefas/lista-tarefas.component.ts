import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Importar FormsModule
import { TarefaService } from '../tarefa.service';
import { Tarefa } from '../tarefa.model';

@Component({
  selector: 'app-lista-tarefas',
  standalone: true,
  imports: [CommonModule, FormsModule], // Adicionar FormsModule aos imports
  templateUrl: './lista-tarefas.component.html',
  styleUrls: ['./lista-tarefas.component.css']
})
export class ListaTarefasComponent implements OnInit {
  tarefas: Tarefa[] = [];
  selectedStatus: string = ''; // Definir valor padrão
  ordenarPor: string = 'dataVencimento'; // Definir valor padrão

  constructor(private tarefaService: TarefaService) { }

  ngOnInit(): void {
    this.loadTarefas();
  }

  onStatusChange(): void {
    this.loadTarefas();
  }

  onOrdenarPorChange(): void {
    this.loadTarefas();
  }

  loadTarefas(): void {
    this.tarefaService.getTarefas(this.selectedStatus, this.ordenarPor).subscribe((data: Tarefa[]) => {
      this.tarefas = data;
    }, error => {
      console.error('Erro ao carregar tarefas', error);
    });
  }

  onDelete(id: number): void {
    if (confirm('Tem certeza que deseja excluir esta tarefa?')) {
      this.tarefaService.deleteTarefa(id).subscribe(() => {
        this.loadTarefas();
      });
    }
  }
}
