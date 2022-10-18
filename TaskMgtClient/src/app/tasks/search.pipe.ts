import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'searchFilter' })
export class FilterPipe implements PipeTransform {
    
  transform(items: any[], searchText: string, pageName: string): any[] {
    if (!items) {
      return [];
    }
    if (!searchText) {
      return items;
    }
    searchText = searchText.toLocaleLowerCase();
    if (pageName == "tasks"){
        return items.filter(it => {
            return it.title.toLocaleLowerCase().includes(searchText);
          });
    }
    return items.filter(it => {
        return it.text.toLocaleLowerCase().includes(searchText);
      });
    
  }
}