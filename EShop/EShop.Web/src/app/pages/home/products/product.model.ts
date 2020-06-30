export default interface Product {
  id: number;
  name: string;
  description: string;
  content: string;
  weight: number;
  category: number | string;
  // url: string;
  numberOfVersions: number;
  price: number;
  originalPrice: number; // TODO: delete?
  discountPrice: number; // TODO: delete?
  quantity: number; // TODO: delete?
  display: boolean; // TODO: delete?
  deliver: boolean;
  applyPromotion: boolean;
}

export interface ProductCategory {
  id: number;
  name: string;
}
