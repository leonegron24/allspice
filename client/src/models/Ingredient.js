import { Recipe } from "./Recipe.js"

export class Ingredient {
    constructor(data) {
        this.createdAt = new Date(data.createdAt)
        this.id = data.id
        this.name = data.name
        this.quantity = data.quantity
        this.recipeId = data.recipeId
        this.recipe = new Recipe(data.recipe)
        this.updatedAt = new Date(data.updatedAt)
    }
}
