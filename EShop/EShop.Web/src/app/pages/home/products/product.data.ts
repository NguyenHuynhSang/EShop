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

let id = 0;
function getId() {
  return id++;
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
    name: "IPhone X",
    image: [
      "https://cdn.fptshop.com.vn/Uploads/Originals/2017/12/8/636483219613202713_3.jpg",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2017/12/8/636483219613202713_1.jpg",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2017/12/8/636483219613046712_2.jpg",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2017/12/8/636483219613046712_4.jpg",
    ],
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
    id: 0,
    name: "Samsung Galaxy A31",
    image: [
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/6/3/637267691826943740_sam-a31-den-1.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/6/3/637267691827023734_sam-a31-den-2.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/4/17/637227211613208786_ss-a31-den-3.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/4/17/637227211613208786_ss-a31-den-4.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/4/17/637227211612739845_ss-a31-den-5.png",
    ],
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
    id: 0,
    name: "IPad Pro 69 XX",
    image: [
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/3/24/637206505930608701_ipad-pro-129-wf-4g-bac-1.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/3/24/637206565432494325_ipad-pro-129-wf-4g-bac-2.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/3/24/637206565430963768_ipad-pro-129-wf-4g-bac-3.png",
    ],
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
    id: 0,
    name: "Macbook Pro 16",
    image: [
      "https://cdn.fptshop.com.vn/Uploads/Originals/2019/11/18/637096945075833818_MBP-16-touch-xam-1.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2019/11/18/637096945074865752_MBP-16-touch-xam-2.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2019/11/18/637096945074865752_MBP-16-touch-xam-3.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2019/11/18/637096945075521293_MBP-16-touch-xam-4.png",
    ],
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
    id: 0,
    name: "HP 15s-fq1109TU i3",
    image: [
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/6/2/637266923420476975_hp-15s-fq-bac-1.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/6/2/637266923420507058_hp-15s-fq-bac-2.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/6/2/637266923420367005_hp-15s-fq-bac-3.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/6/2/637266923420026979_hp-15s-fq-bac-4.png",
    ],
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
    id: 0,
    name: "Huawei MatePad T",
    image: [
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/5/27/637261743734681472_huawei-matepad-t-1.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/5/27/637261743734651535_huawei-matepad-t-2.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/5/27/637261743733991550_huawei-matepad-t-3.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2020/5/27/637261743734491456_huawei-matepad-t-4.png",
    ],
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
    id: 0,
    name: "Samsung Galaxy A70",
    image: [
      "https://cdn.fptshop.com.vn/Uploads/Originals/2019/4/13/636907475981220637_samsung-galaxy-a70-den-1.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2019/4/13/636907475980100637_samsung-galaxy-a70-den-2.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2019/4/13/636907475976810637_samsung-galaxy-a70-den-3.png",
      "https://cdn.fptshop.com.vn/Uploads/Originals/2019/4/13/636907475976490637_samsung-galaxy-a70-den-4.png",
    ],
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

const increment = (products: Product[], number: number) => {
  return products.map((p) => ({
    ...p,
    id: getId(),
    name: p.name + " " + number,
  }));
};

export default JSON.parse(
  JSON.stringify([
    ...increment(products, 0),
    ...increment(products, 1),
    ...increment(products, 2),
    ...increment(products, 3),
    ...increment(products, 4),
  ])
) as Product[];
