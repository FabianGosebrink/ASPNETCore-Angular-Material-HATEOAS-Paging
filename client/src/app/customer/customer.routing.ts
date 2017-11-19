import { Routes } from '@angular/router';

import { DetailsComponent } from './details/details.component';
import { OverviewComponent } from './overview/overview.component';

export const CustomerRoutes: Routes = [
    { path: 'all', component: OverviewComponent },
    { path: 'details', component: DetailsComponent },
    { path: 'details/:id', component: DetailsComponent }
];
