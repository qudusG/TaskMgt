import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClipboardModule } from 'ngx-clipboard';
import { CreateTaskComponent } from '../create-task/create-task.component';
import { TaskDetailComponent } from '../task-detail/task-detail.component';
import { TasksComponent } from '../tasks/tasks.component';
import { AppLayoutComponent } from './app-layout.component';
//import { AppLayoutRoutingModule, routes } from './app-layout-routing.module';
import { ComponentsModule } from '../components/components.module';
import { RouterModule } from '@angular/router';
import { routes } from './app-layout-routing.module';
import { TokenInterceptorService } from '../auth-layout/token.interceptor';
import { StatusPipe } from '../tasks/status.pipe';
import { StatusBGPipe } from '../tasks/statusbg.pipe';
import { FilterPipe } from '../tasks/search.pipe';

@NgModule({
  declarations: [
    TasksComponent,
    TaskDetailComponent,
    CreateTaskComponent,
    StatusPipe,
    StatusBGPipe,
    FilterPipe
  ],
  imports: [
    CommonModule,
    //AppLayoutRoutingModule,
    FormsModule,
    ComponentsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    NgbModule,
    ClipboardModule
  ],
  exports: [RouterModule],
  bootstrap: [AppLayoutComponent],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    }
  ],
})
export class AppLayoutModule { }
