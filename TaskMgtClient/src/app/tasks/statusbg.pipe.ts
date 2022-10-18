import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'statusBg'
})

export class StatusBGPipe implements PipeTransform {
    transform(value: number): string {
        if (value == 0) return "info";
        if (value == 1) return "warning";
        return "success";
    }
}
