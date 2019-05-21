import { Routes } from '@angular/router';

export const AppRoutes: Routes = [
    { path: '', redirectTo: 'all', pathMatch: 'full' },
    {
        path: '**',
        redirectTo: 'all'
    }
];
