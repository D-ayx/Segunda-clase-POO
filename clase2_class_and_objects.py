class Persona: 
    def __init__(self,nombre,edad): 
        self.nombre=nombre
        self.edad=edad
    def saludar(self): 
        return f"hola, soy{ self.nombre}"

            # uso
p = Persona("Dayanna", 19)
print(p.saludar())
