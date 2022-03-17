import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CategoriasComponent } from './categorias/categorias.component';
import { CategoriasDetalheComponent } from './categorias-detalhe/categorias-detalhe.component';
import { CategoriasNovaComponent } from './categorias-nova/categorias-nova.component';
import { CategoriasEditarComponent } from './categorias-editar/categorias-editar.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';

@NgModule({
  declarations: [
    AppComponent,
    CategoriasComponent,
    CategoriasDetalheComponent,
    CategoriasNovaComponent,
    CategoriasEditarComponent,
    LoginComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
