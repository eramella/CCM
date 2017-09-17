import { Tag } from "./ITag";

export interface Session {
    description: string,
    id: string,
    level: number,
    levelName: string,
    title: string,
    campId: number,
    tags: string[],
    userId: string,
    userFirstName: string,
    userLastName: string
}