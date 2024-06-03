export class ItemDTO {
    itemId: number = 0;
    itemName: string = '';
    itemCode: string = '';
    storeId: number = 0;
    storeName: string = '';
    stockRemaining: number = 0;
}

export class NewItem {
    itemName: string = '';
    brandName: string = '';
    unitOfMeasurement: string = '';
    purchaseRate: number = 0;
    salesRate: number = 0;
    quantity: number = 0;
    expiryDate: Date | undefined;
    storeName: string = '';
}

