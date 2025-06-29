import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-tutorial',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tutorial.component.html',
  styleUrl: './tutorial.component.scss'
})
export class TutorialComponent {
  steps = [
    {
      title: 'Step 1: Describe Your Logic',
      description: 'Use natural language like "Print numbers 1 to 10" in the input box to generate logic blocks.',
      icon: '📝'
    },
    {
      title: 'Step 2: Generate Blocks',
      description: 'Click "Generate Blocks" to convert your description into visual Blockly blocks using AI.',
      icon: '⚙️'
    },
    {
      title: 'Step 3: Use the Blocks',
      description: `Drag blocks from the workspace, snap them together, update values, and right-click to delete or duplicate them.`,
      icon: '📦'
    },
    {
      title: 'Step 4: Customize Your Logic',
      description: 'Modify the generated blocks or add new ones from the toolbox to refine your logic.',
      icon: '🔧'
    },
    {
      title: 'Step 5: Run and See Output',
      description: 'Click "Run Code" to execute your blocks and view the JavaScript output and console result.',
      icon: '🚀'
    },
    {
      title: 'Step 6: Try Daily Challenges',
      description: 'Sharpen your skills by solving fun daily logic challenges. Use hints if you get stuck!',
      icon: '🎯'
    },
    {
      title: 'Step 7: Sign In for Progress',
      description: 'Create an account or sign in to track your completed challenges and learning streaks.',
      icon: '🔐'
    }
  ];
}
