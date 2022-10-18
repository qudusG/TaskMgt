import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClipboardModule } from 'ngx-clipboard';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentsModule } from './components/components.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthLayoutComponent } from './auth-layout/auth-layout.component';
import { AppLayoutComponent } from './app-layout/app-layout.component';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { TokenInterceptorService } from './auth-layout/token.interceptor';
import { FilterPipe } from './tasks/search.pipe';

@NgModule({
  declarations: [
    AppComponent,
    AuthLayoutComponent,
    AppLayoutComponent
  ],
  imports: [
    BrowserModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgbModule,
    ClipboardModule,
    CommonModule,
    BrowserAnimationsModule,
    FormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
