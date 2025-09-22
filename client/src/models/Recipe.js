import { Account } from "./Account.js"

export class Recipe {
    constructor(data) {
        this.category = data.category
        this.createdAt = new Date(data.createdAt)
        this.creator = new Account(data.creator)
        this.creatorId = data.creatorId
        this.favoriteCount = data.favoriteCount
        this.id = data.id
        this.img = data.img
        this.instructions = data?.instructions
        this.title = data.title
        this.updatedAt = new Date(data.updatedat)
    }
}

