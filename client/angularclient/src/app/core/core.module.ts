import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { CustomerDataService } from './services/customer-data.service';
import { PaginationService } from './services/pagination.service';

@NgModule({
    imports: [HttpClientModule],
    exports: [],
    declarations: [],
    providers: [
        CustomerDataService,
        PaginationService
    ],
})

export class CoreModule { }
