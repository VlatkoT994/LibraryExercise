import { throws } from 'assert';

export class ForEdit {
  key: string;
  type: string;
  value: any;

  constructor(key, type, value) {
    this.key = key;
    this.type = type;
    this.value = value;
  }
}

export class PatchDoc {
  op: string;
  path: string;
  value: string;

  constructor(path: string, value) {
    this.op = 'replace';
    this.path = `/${path}`;
    this.value = value;
  }
}
