import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerDataService } from '../../core/services/customer-data.service';
import { Customer } from '../../models/customer.model';

@Component({
  selector: 'app-details',
  templateUrl: 'details.component.html'
})
export class DetailsComponent implements OnInit {
  customer = new Customer();

  constructor(
    private customerDataService: CustomerDataService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.customerDataService
        .getSingle<Customer>(+id)
        .subscribe(customer => (this.customer = customer));
    }
  }

  save() {
    const method = this.customer.id ? 'PUT' : 'POST';

    this.customerDataService
      .fireRequest(this.customer, method)
      .subscribe(() => this.router.navigate(['all']));
  }
}
