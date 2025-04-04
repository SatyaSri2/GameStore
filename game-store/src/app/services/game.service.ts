import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private apiUrl = 'https://localhost:5055/api/games';

  constructor(private http: HttpClient) {}

  getGames(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
