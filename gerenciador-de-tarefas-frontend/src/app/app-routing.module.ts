import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListaTarefasComponent } from './tarefa/lista-tarefas/lista-tarefas.component';
import { FormularioTarefaComponent } from './tarefa/formulario-tarefa/formulario-tarefa.component';
import { EditarTarefaComponent } from './tarefa/editar-tarefa/editar-tarefa.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth/authguard';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'tarefas', component: ListaTarefasComponent, canActivate: [AuthGuard] },
  { path: 'nova-tarefa', component: FormularioTarefaComponent, canActivate: [AuthGuard] },
  { path: 'editar-tarefa', component: EditarTarefaComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
