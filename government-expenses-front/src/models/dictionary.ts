type Dictionary<T = any> = {[index : string] : T};
export type StringDictionary = Dictionary<string>;
export type NumberDictionary = Dictionary<number>;
export type BoolDictionary = Dictionary<boolean>;
export default Dictionary;