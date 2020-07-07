import Product, { ProductCategory } from "./product.model";
import { LoremIpsum } from "lorem-ipsum";
import { randomBetween, randomBoolean } from "../helpers/random";

const descriptionGen = new LoremIpsum({
  sentencesPerParagraph: {
    max: 8,
    min: 4,
  },
  wordsPerSentence: {
    max: 16,
    min: 4,
  },
});
const contentGen = new LoremIpsum({
  sentencesPerParagraph: {
    max: 3,
    min: 1,
  },
  wordsPerSentence: {
    max: 20,
    min: 4,
  },
});

function getDescription() {
  return descriptionGen.generateParagraphs(randomBetween(3, 5));
}

function getContent() {
  return contentGen.generateParagraphs(randomBetween(8, 14));
}

export const productCategories: ProductCategory[] = [
  { id: 0, name: "Samsung" },
  { id: 1, name: "Huawei" },
  { id: 2, name: "HP" },
  { id: 3, name: "Macbook" },
  { id: 4, name: "IPad" },
  { id: 5, name: "IPhone" },
];

const products: Product[] = [
  {
    id: 0,
    name: "IPhone XX",
    description: getDescription(),
    content: getContent(),
    weight: 0.5,
    category: productCategories[5],
    numberOfVersions: 4,
    price: 26000000,
    originalPrice: 20000000,
    discountPrice: 23000000,
    quantity: 432,
    display: randomBoolean(),
    deliver: randomBoolean(),
    applyPromotion: randomBoolean(),
  },
  {
    id: 1,
    name: "Samsung galaxy X",
    description: getDescription(),
    content: getContent(),
    weight: 0.7,
    category: productCategories[0],
    numberOfVersions: 3,
    price: 26000000,
    originalPrice: 20000000,
    discountPrice: 23000000,
    quantity: 308,
    display: randomBoolean(),
    deliver: randomBoolean(),
    applyPromotion: randomBoolean(),
  },
  {
    id: 2,
    name: "IPad Pro 69 XX",
    description: getDescription(),
    content: getContent(),
    weight: 0.8,
    category: productCategories[4],
    numberOfVersions: 3,
    price: 25000000,
    originalPrice: 22000000,
    discountPrice: 24000000,
    quantity: 76,
    display: randomBoolean(),
    deliver: randomBoolean(),
    applyPromotion: randomBoolean(),
  },
  {
    id: 3,
    name: "Macbook Pro 16'",
    description: getDescription(),
    content: getContent(),
    weight: 1.5,
    category: productCategories[3],
    numberOfVersions: 2,
    price: 40000000,
    originalPrice: 35000000,
    discountPrice: 37000000,
    quantity: 160,
    display: randomBoolean(),
    deliver: randomBoolean(),
    applyPromotion: randomBoolean(),
  },
  {
    id: 4,
    name: "HP 15 da0054TU i3 7020U (4ME68PA)",
    description: getDescription(),
    content: getContent(),
    weight: 1.7,
    category: productCategories[2],
    numberOfVersions: 8,
    price: 5000000,
    originalPrice: 3000000,
    discountPrice: 3750000,
    quantity: 234,
    display: randomBoolean(),
    deliver: randomBoolean(),
    applyPromotion: randomBoolean(),
  },
  {
    id: 5,
    name: "Huawei MatePad T8",
    description: getDescription(),
    content: getContent(),
    weight: 1.4,
    category: productCategories[1],
    numberOfVersions: 4,
    price: 3290000,
    originalPrice: 1500000,
    discountPrice: 2000000,
    quantity: 485,
    display: randomBoolean(),
    deliver: randomBoolean(),
    applyPromotion: randomBoolean(),
  },
  {
    id: 6,
    name: "Samsung Galaxy A70",
    description: getDescription(),
    content: getContent(),
    weight: 0.6,
    category: productCategories[0],
    numberOfVersions: 5,
    price: 4400000,
    originalPrice: 3000000,
    discountPrice: 3500000,
    quantity: 431,
    display: randomBoolean(),
    deliver: randomBoolean(),
    applyPromotion: randomBoolean(),
  },
];

export default products;
