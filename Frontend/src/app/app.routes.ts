import { Routes } from '@angular/router';
import { AboutusComponent } from './shared/components/aboutus/aboutus.component';

export const routes: Routes = [
  { path: '', redirectTo: 'blockwise', pathMatch: 'full' },
  { path: 'auth', loadChildren: () => import('./feature/auth/auth.module').then(m => m.AuthModule) },
  { path: 'blockwise', loadChildren: () => import('./feature/blockly/blockly.module').then(m => m.BlocklyModule)},
  { path: 'about-us', component:AboutusComponent }
];
