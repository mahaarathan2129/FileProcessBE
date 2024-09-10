import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GlobalVariablesService {
  constructor() { }
  
  public LOGIN_URL = `/Auth/Login`;
  public FILE_URL = `/File`;
  
}
