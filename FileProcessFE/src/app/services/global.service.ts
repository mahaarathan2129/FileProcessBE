import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GlobalService {
  constructor() { }

  setLocalStorage(key: string, value: string) {
    localStorage.setItem(key, value);
  }
  getLocalStorage(key: string) {
    return localStorage.getItem(key);
  }

  clearLocalStorage() {
    return localStorage.clear();
  }
}

