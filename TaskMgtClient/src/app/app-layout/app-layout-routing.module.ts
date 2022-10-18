import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateTaskComponent } from '../create-task/create-task.component';
import { TaskDetailComponent } from '../task-detail/task-detail.component';
import { TasksComponent } from '../tasks/tasks.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'tasks',
  },
  {
    path: 'tasks',
    component: TasksComponent
  },
  {
    path: 'tasks/:id',
    component: TaskDetailComponent
  },
  {
    path: 'create-task',
    component: CreateTaskComponent
  }
];

/* @NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppLayoutRoutingModule { } */
