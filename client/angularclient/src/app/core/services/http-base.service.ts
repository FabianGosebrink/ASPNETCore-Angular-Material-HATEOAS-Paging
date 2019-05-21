import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginationService } from './pagination.service';

@Injectable()
export class HttpBaseService {
  private headers = new HttpHeaders();
  private endpoint = `https://localhost:44380/api/customers/`;

  constructor(
    private httpClient: HttpClient,
    private paginationService: PaginationService
  ) {
    this.headers = this.headers.set('Content-Type', 'application/json');
    this.headers = this.headers.set('Accept', 'application/json');
  }

  getAll<T>() {
    const mergedUrl =
      `${this.endpoint}` +
      `?page=${this.paginationService.page}&pageCount=${
        this.paginationService.pageCount
      }`;

    return this.httpClient.get<T>(mergedUrl, { observe: 'response' });
  }

  getSingle<T>(id: number) {
    return this.httpClient.get<T>(`${this.endpoint}${id}`);
  }

  add<T>(toAdd: T) {
    return this.httpClient.post<T>(this.endpoint, toAdd, {
      headers: this.headers
    });
  }

  update<T>(url: string, toUpdate: T) {
    return this.httpClient.put<T>(url, toUpdate, { headers: this.headers });
  }

  delete(url: string) {
    return this.httpClient.delete(url);
  }
}
