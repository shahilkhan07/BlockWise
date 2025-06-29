import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', 
    loadComponent: () =>
      import('./blockly-editor/blockly-editor.component').then(
        m => m.BlocklyEditorComponent
      )
  },
  { path: 'tutorial', 
    loadComponent: () =>
      import('./tutorial/tutorial.component').then(
        m => m.TutorialComponent
      )
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BlocklyRoutingModule {}
