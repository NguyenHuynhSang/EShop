export default function moveArrayItem(array: any[], fromIndex: number, toIndex: number) {
    var element = array[fromIndex];
    array.splice(fromIndex, 1);
    array.splice(toIndex, 0, element);
    return [...array]
}