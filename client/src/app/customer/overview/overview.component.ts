import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { map, filter } from 'rxjs/operators';

import { CustomerDataService } from '../../core/services/customer-data.service';
import { PaginationService } from '../../core/services/pagination.service';
import { Customer } from '../../models/customer.model';

@Component({
    selector: 'app-overview',
    templateUrl: 'overview.component.html'
})

export class OverviewComponent implements OnInit {

    dataSource: Customer[];
    totalCount: number;

    constructor(
        private customerDataService: CustomerDataService,
        private paginationService: PaginationService) { }

    ngOnInit(): void {
        this.getAllCustomers();
    }

    switchPage(event: PageEvent) {
        this.paginationService.change(event);
        this.getAllCustomers();
    }

    delete(customer: Customer) {
        this.customerDataService.fireRequest(customer, 'DELETE')
            .subscribe(() => {
                this.dataSource = this.dataSource.filter(x => x.id !== customer.id);
            });
    }

    getAllCustomers() {
        this.customerDataService.getAll<Customer[]>()
            .subscribe((result: any) => {
                this.totalCount = JSON.parse(result.headers.get('X-Pagination')).totalCount;
                this.dataSource = result.body.value;
            });
    }
}
