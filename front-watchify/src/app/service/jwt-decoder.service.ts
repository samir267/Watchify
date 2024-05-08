import { Injectable } from '@angular/core';
import { json } from 'body-parser';

@Injectable({
  providedIn: 'root'
})
export class JwtDecoderService {

  constructor() { }

public decodeToken1(token: string) {
  const base64Url = token.split('.')[1]; // Extract payload part
  const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/'); // Replace characters
  const jsonPayload = atob(base64); // Decode base64
  return JSON.parse(jsonPayload); // Parse JSON
}

}
