import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriasDetalhesComponent } from './categorias-detalhes.component';

describe('CategoriasDetalhesComponent', () => {
  let component: CategoriasDetalhesComponent;
  let fixture: ComponentFixture<CategoriasDetalhesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategoriasDetalhesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriasDetalhesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
