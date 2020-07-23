import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root',
})
export class ApiService {
  url = `https://localhost:44363/api/`;
  headers = new HttpHeaders().set('content-type', 'application/json');
  constructor(private http: HttpClient) {}
  getLibrary(libraryId) {
    return this.http.get(`${this.url}libraries/${libraryId}`);
  }
  getLibraries() {
    return this.http.get(`${this.url}libraries`);
  }
  patchLibrary(libId: number, patchDoc: object[]) {
    return this.http.patch(
      `${this.url}libraries/${libId}`,
      JSON.stringify(patchDoc),
      { headers: this.headers }
    );
  }
  deleteLibrary(libraryId: string) {
    return this.http.delete(`${this.url}libraries/${libraryId}`);
  }
  getClients(searchParam: string) {
    return this.http.get(`${this.url}clients?name=${searchParam}`);
  }
}
