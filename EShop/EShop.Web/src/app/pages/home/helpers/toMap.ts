/**
 * ```
 * const input = [{ id: 1, name: 'one' }, { id: 2, name: 'two' }, { id: 3, name: 'three' }]
 * const output = toMap(input, 'id', 'name');
 * // { id: 1, name: 'one', id: 2, name: 'two', id: 3, name: 'three' }
 * ```
 * @param array array of objects to convert to a map
 * @param keyPath the path of the key property
 * @param valuePath the path of the value property
 */
export default function toMap(
  array: Object[],
  keyPath: string,
  valuePath: string
) {
  const mappings: { [key: string]: string } = {};

  array.forEach((o) => {
    const key = o[keyPath];
    const value = o[valuePath];

    mappings[key] = value;
  });

  return mappings;
}
