import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriasDetalheComponent } from './categorias-detalhe.component';

describe('CategoriasDetalheComponent', () => {
  let component: CategoriasDetalheComponent;
  let fixture: ComponentFixture<CategoriasDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategoriasDetalheComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriasDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
