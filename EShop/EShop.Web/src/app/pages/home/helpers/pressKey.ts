import has from 'lodash/has';

type KeyEvent = 'keydown' | 'keyup' | 'keypress';

export const VKey = {
  Escape: 27,
};

export default function pressKey(keyEvent: KeyEvent, keyCode: number) {
  const keyboardEvent = document.createEvent('KeyboardEvent');
  const initMethod = has(keyboardEvent, 'initKeyboardEvent')
    ? 'initKeyboardEvent'
    : 'initKeyEvent';

  keyboardEvent[initMethod](
    keyEvent,
    true, // bubbles
    true, // cancelable
    window, // view: should be window
    false, // ctrlKey
    false, // altKey
    false, // shiftKey
    false, // metaKey
    keyCode, // keyCode: unsigned long - the virtual key code, else 0
    0 // charCode: unsigned long - the Unicode character associated with the depressed key, else 0
  );
  document.dispatchEvent(keyboardEvent);
}
