import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BlocklyService {
  private apiUrl = 'http://localhost:59500/v1/Blockly';

  constructor(private http: HttpClient) {}

  generateBlocks(prompt: string): Observable<{ xml: string }> {
    return this.http.post<{ xml: string }>(`${this.apiUrl}/generate-blocks`, {
      text: prompt
    });
  }
}
