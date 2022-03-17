import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriasNovaComponent } from './categorias-nova.component';

describe('CategoriasNovaComponent', () => {
  let component: CategoriasNovaComponent;
  let fixture: ComponentFixture<CategoriasNovaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CategoriasNovaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriasNovaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
