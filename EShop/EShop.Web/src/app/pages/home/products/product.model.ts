export default interface Product {
    id: number;
    name: string;
    category: number | string;
    numberOfVersions: number;
    price: number;
    originalPrice: number;
    discountPrice: number;
    quantity: number;
    display: boolean;
}

export interface ProductCategory {
    id: number;
    name: string;
}