import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tarefa } from './tarefa.model';

@Injectable({
  providedIn: 'root'
})
export class TarefaService {
  private apiUrl = 'https://localhost:7252/api/tarefas';

  constructor(private http: HttpClient) { }

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('jwtToken');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  getTarefas(status: string = '', ordenarPor: string = 'dataVencimento'): Observable<Tarefa[]> {
    let params = new HttpParams();
    if (status) params = params.set('status', status);
    if (ordenarPor) params = params.set('ordenarPor', ordenarPor);
    return this.http.get<Tarefa[]>(this.apiUrl, { headers: this.getAuthHeaders(), params });
  }

  getTarefaById(id: number): Observable<Tarefa> {
    return this.http.get<Tarefa>(`${this.apiUrl}/${id}`, { headers: this.getAuthHeaders() });
  }

  addTarefa(tarefa: Tarefa): Observable<Tarefa> {
    return this.http.post<Tarefa>(this.apiUrl, tarefa, { headers: this.getAuthHeaders() });
  }

  updateTarefa(id: number, tarefa: Tarefa): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, tarefa, { headers: this.getAuthHeaders() });
  }

  deleteTarefa(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers: this.getAuthHeaders() });
  }
}
