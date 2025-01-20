import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormularioTarefaComponent } from './formulario-tarefa.component';

describe('FormularioTarefaComponent', () => {
  let component: FormularioTarefaComponent;
  let fixture: ComponentFixture<FormularioTarefaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FormularioTarefaComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(FormularioTarefaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
