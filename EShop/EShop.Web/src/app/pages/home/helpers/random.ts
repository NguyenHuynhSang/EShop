import { sub } from 'date-fns';

const now = Date.now();

export function randomBetween(min: number, max: number) {
  // min and max included
  return Math.floor(Math.random() * (max - min + 1) + min);
}

export function randomBoolean() {
  return Math.random() > 0.5 ? true : false;
}

export function randomDateBeforeNow(maxDaysBefore: number) {
  const duration: Duration = {
    seconds: randomBetween(0, 59),
    minutes: randomBetween(0, 59),
    hours: randomBetween(0, 23),
    days: randomBetween(1, maxDaysBefore),
  };
  return sub(now, duration);
}
