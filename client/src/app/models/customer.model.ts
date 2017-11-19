export class Customer {
    id: number;
    name: string;
    created: Date;
    links: Link[];
}

export class Link {
    href: string;
    rel: string;
    method: string;
}
