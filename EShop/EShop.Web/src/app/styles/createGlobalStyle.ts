import React from 'react';

// credit to styled-component
// https://github.com/styled-components/styled-components/blob/f0a84f418e2625a0c2a4adf73dc42e93c8992f67/packages/styled-components/src/sheet/dom.js#L24
function findLastStyleTag(): HTMLStyleElement | undefined {
  const { childNodes } = document.head;
  const elementType = 1;

  for (let i = childNodes.length; i >= 0; i--) {
    const child = childNodes[i];
    if (child?.nodeType === elementType && child instanceof HTMLStyleElement) {
      return child;
    }
  }

  return undefined;
}

function interleave(strings: TemplateStringsArray, interpolations: any[]) {
  const result = [strings[0]];

  for (let i = 0, len = interpolations.length; i < len; i += 1) {
    result.push(interpolations[i], strings[i + 1]);
  }

  return result;
}

export default function createGlobalStyle(
  styles: TemplateStringsArray,
  ...interpolations: any[]
) {
  return React.memo(() => {
    const styleElement = document.createElement('style');
    const style = interleave(styles, interpolations).join('');

    styleElement.innerHTML = style;

    // erase second argument type since it's actually optional but typescript requires for some reasons
    document.head.insertBefore(
      styleElement,
      findLastStyleTag()?.nextSibling as any
    );

    return null;
  });
}
