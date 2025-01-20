import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private snackBar: MatSnackBar) { }

  canActivate(): boolean {
    const isLoggedIn = !!localStorage.getItem('isLoggedIn');

    if (!isLoggedIn) {
      this.snackBar.open('Você precisa estar logado para acessar esta funcionalidade.', 'Fechar', {
        duration: 3000
      });
      this.router.navigate(['/home']);
      return false;
    }

    return true;
  }
}
