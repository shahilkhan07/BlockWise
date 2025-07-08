import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BlocklyService } from '../../services/blockly.service';
import * as Blockly from 'blockly';
import { HttpClientModule } from '@angular/common/http';
import 'blockly/javascript_compressed.js';
import { javascriptGenerator } from 'blockly/javascript';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-blockly-editor',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './blockly-editor.component.html',
  styleUrls: ['./blockly-editor.component.scss']
})
export class BlocklyEditorComponent implements OnInit {
  @ViewChild('blocklyDiv', { static: true }) blocklyDiv!: ElementRef;

  challenges = [
    {
      question: 'Print numbers from 1 to 10',
      hint: 'Use a repeat loop from 0 to 9 and print the value plus 1.',
      showHint: false
    },
    {
      question: 'Reverse print numbers from 10 to 1',
      hint: 'Use a loop that counts down and prints each number.',
      showHint: false
    },
    {
      question: 'Repeat the word "hello" 5 times',
      hint: 'Use a repeat block with a print block inside.',
      showHint: false
    },
    {
      question: 'Print characters of a word one by one',
      hint: 'Use a loop and text.charAt(index) to access each character.',
      showHint: false
    }
  ];

  currentChallengeIndex = -1;
  currentChallenge: any = null;
  showHintText = false;
  userPrompt = '';
  workspace!: Blockly.WorkspaceSvg;
  generatedCode = '';
  executionOutput = '';
  activeTab: 'code' | 'output' = 'code';
  statusMessage: string = 'Ready to generate blocks';
  isLoading: boolean = false;
  isLoggedIn: boolean = false;
  private authSubscription!: Subscription;

  constructor(private blocklyService: BlocklyService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {

    this.authSubscription = this.authService.loggedIn$.subscribe((status) => {
      this.isLoggedIn = status;
    });

    Blockly.Blocks['say_hello'] = {
      init: function () {
        this.appendDummyInput().appendField("Say Hello");
        this.setPreviousStatement(true, null);
        this.setNextStatement(true, null);
        this.setColour(160);
        this.setTooltip("Logs a hello message.");
        this.setHelpUrl("");
      }
    };

    javascriptGenerator.forBlock['say_hello'] = function () {
      return 'console.log("Hello from custom block!");\n';
    };

    javascriptGenerator.forBlock['text_print'] = function (block: any, generator: any) {
      const msg = generator.valueToCode(block, 'TEXT', javascriptGenerator.ORDER_NONE) || "''";
      return `console.log(${msg});\n`;
    };

    this.workspace = Blockly.inject(this.blocklyDiv.nativeElement, {
      toolbox: `<xml xmlns="https://developers.google.com/blockly/xml">
        <category name="Logic" colour="#5C81A6">
          <block type="controls_if"></block>
          <block type="logic_compare"></block>
          <block type="logic_boolean"></block>
        </category>

        <category name="Loops" colour="#5CA65C">
          <block type="controls_repeat_ext"></block>
          <block type="controls_whileUntil"></block>
        </category>

        <category name="Math" colour="#5C68A6">
          <block type="math_number"></block>
          <block type="math_arithmetic"></block>
        </category>

        <category name="Text" colour="#5CA6A6">
          <block type="text"></block>
          <block type="text_join"></block>
          <block type="text_length"></block>
          <block type="text_charAt"></block>
          <block type="text_print"></block>
        </category>

        <category name="Variables" custom="VARIABLE" colour="#A65C81"></category>

        <category name="Custom Blocks" colour="#D65C5C">
          <block type="say_hello"></block>
        </category>
      </xml>`
    });
  }

  startChallenge(): void {
    if (this.currentChallenge) {
      this.currentChallenge = null;
      this.currentChallengeIndex = -1;
      this.showHintText = false;
    } else {
      this.currentChallengeIndex = 0;
      this.currentChallenge = this.challenges[this.currentChallengeIndex];
      this.showHintText = false;
    }
  }

  nextChallenge(): void {
    this.currentChallengeIndex = (this.currentChallengeIndex + 1) % this.challenges.length;
    this.currentChallenge = this.challenges[this.currentChallengeIndex];
    this.showHintText = false;
  }

  toggleHint(): void {
    this.showHintText = !this.showHintText;
  }

  useChallenge(question: string): void {
    this.userPrompt = question;
  }

  showHint(challenge: any): void {
    challenge.showHint = !challenge.showHint;
  }

  generateBlocks(): void {
    if (!this.userPrompt.trim()) return;

    this.isLoading = true;
    this.statusMessage = 'Generating blocks...';

    this.blocklyService.generateBlocks(this.userPrompt).subscribe({
      next: (response) => {
        const xmlText = response.xml.trim();
        const parser = new DOMParser();
        const xmlDom = parser.parseFromString(xmlText, 'text/xml').documentElement;

        Blockly.Xml.clearWorkspaceAndLoadFromXml(xmlDom, this.workspace);

        this.statusMessage = 'Blocks generated successfully!';
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error generating blocks:', err);
        this.statusMessage = 'Error generating blocks.';
        this.isLoading = false;
      }
    });
  }

  runBlocks(): void {
    if (!this.workspace) return;

    try {
      const code = javascriptGenerator.workspaceToCode(this.workspace);
      this.generatedCode = code;

      let output = '';
      const originalLog = console.log;
      console.log = (...args: any[]) => {
        output += args.join(' ') + '\n';
        originalLog.apply(console, args);
      };

      eval(code);
      console.log = originalLog;

      this.executionOutput = output || 'Code executed successfully.';
      this.activeTab = 'output';
    } catch (err: any) {
      this.executionOutput = 'Error: ' + err.message;
      this.activeTab = 'output';
    }
  }
}
