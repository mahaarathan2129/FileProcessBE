import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PagenotfoundComponent } from './@core/common/component/pagenotfound/pagenotfound.component';
import { AuthGuard } from './auth/auth.guard';
const auth = () => import('./auth/auth.module').then(X => X.AuthModule)
const pages = () => import('./pages/pages.module').then(X => X.PagesModule)

const routes: Routes = [
  { path: '', loadChildren: pages,canActivate:[AuthGuard]},
  { path: 'auth', loadChildren: auth },
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
