export default interface Product {
  id: number;
  name: string;
  image: string[];
  description: string;
  content: string;
  weight: number;
  category: ProductCategory;
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

export interface ProductResult {
  results: Product[];
  totalResults: number;
}