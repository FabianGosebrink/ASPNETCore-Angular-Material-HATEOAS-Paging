import { Injectable } from '@angular/core';

import { Customer } from '../../models/customer.model';
import { HttpBaseService } from './http-base.service';

@Injectable()
export class CustomerDataService extends HttpBaseService {

    fireRequest(customer: Customer, method: string) {

        const links = customer.links
            ? customer.links.find(x => x.method === method)
            : null;

        switch (method) {
            case 'DELETE': {
                return super.delete(links.href);
            }
            case 'POST': {
                return super.add<Customer>(customer);
            }
            case 'PUT': {
                return super.update<Customer>(links.href, customer);
            }
            default: {
                console.log(`${links.method} not found!!!`);
                break;
            }
        }
    }
}
