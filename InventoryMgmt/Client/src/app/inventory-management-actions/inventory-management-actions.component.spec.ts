import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InventoryManagementActionsComponent } from './inventory-management-actions.component';

describe('InventoryManagementActionsComponent', () => {
  let component: InventoryManagementActionsComponent;
  let fixture: ComponentFixture<InventoryManagementActionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InventoryManagementActionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InventoryManagementActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
