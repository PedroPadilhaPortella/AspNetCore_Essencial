import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoriasComponent } from './categorias/categorias.component';
import { CategoriasDetalhesComponent } from './categorias-detalhes/categorias-detalhes.component';
import { CategoriasNovaComponent } from './categorias-nova/categorias-nova.component';
import { CategoriasEditarComponent } from './categorias-editar/categorias-editar.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';

@NgModule({
  declarations: [
    AppComponent,
    CategoriasComponent,
    CategoriasDetalhesComponent,
    CategoriasNovaComponent,
    CategoriasEditarComponent,
    LoginComponent,
    LogoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
