import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { PaginationService } from '../../core/services/pagination.service';
import { Customer } from '../../models/customer.model';

@Component({
    selector: 'app-list',
    templateUrl: 'list.component.html'
})

export class ListComponent {

    dataSource = new MatTableDataSource<Customer>();
    displayedColumns = ['id', 'name', 'created', 'actions'];

    @Input('dataSource')
    set allowDay(value: Customer[]) {
        this.dataSource = new MatTableDataSource<Customer>(value);
    }

    @Input() totalCount: number;
    @Output() onDeleteCustomer = new EventEmitter();
    @Output() onPageSwitch = new EventEmitter();
    @Output() onRedirectToDetails = new EventEmitter();

    constructor(public paginationService: PaginationService) { }
}
