import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root',
})
export class ApiService {
  url = `https://localhost:44363/api/`;
  constructor(private http: HttpClient) {}
  getLibrary(libraryId) {
    return this.http.get(`${this.url}libraries/${libraryId}`);
  }
}
