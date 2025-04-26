import { DecimalPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Game {
  id?: number;
  name: string;
  category: string;
  price: DecimalPipe;
  description: string;
  image: string;
  rating: number;
  reviews: number;
  downloads: number;
  badge: string;
  genre: string;
  isFavorite: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private apiUrl = 'http://localhost:5055/api/games';

  constructor(private http: HttpClient) {}

  getGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.apiUrl);
  }

  addGame(game: Game): Observable<Game> {
    return this.http.post<Game>(this.apiUrl, game);
  }

  deleteGame(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
