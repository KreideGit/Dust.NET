#version 330 core
layout (location = 0) in vec3 position; 
layout (location = 1) in vec2 tex;
  
out vec2 o_tex;
  
void main()
{
    o_tex = tex;
    gl_Position = vec4(position, 1.0); 
}