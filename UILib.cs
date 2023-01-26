using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using MainClassName;

namespace Engine.UI
{
    public class Button : EngineMainClass.ICanBeDrawed
    {
        public RectangleShape shape;
        public Transform transform;
        public Texture texture;
        public Color color = Color.White;
        //private View Camera = MainClass.Camera;
        public Action OnClick;
        public Action OnCursorEnter;
        public Action OnCursorExit;
        public bool active { get; private set; } = true;

        //private Vector2f Offset;
        private bool WasCursorButton;
        private bool WasCursorNonButton;
   
        //Только позиция
        public Button(Vector2f position)
        {
            shape = new RectangleShape(new Vector2f(16, 16));
            SetPos(position);
            shape.Scale = new Vector2f(1, 1);
            shape.Origin = new Vector2f(8, 8);

            transform.position = shape.Position;
            transform.scale = shape.Scale;
            World.WorldUIObjects.Add(this);
        }
        //Позиция и размер
        public Button(Vector2f position, Vector2f scale)
        {
            shape = new RectangleShape(new Vector2f(16, 16));
            SetPos(position);
            shape.Scale = scale;
            shape.Origin = scale / 2;

            transform.position = shape.Position;
            transform.scale = shape.Scale;
            World.WorldUIObjects.Add(this);
        }        
        //Позиция, размер и текстура
        public Button(Vector2f position, Vector2f scale, Texture texture)
        {
            shape = new RectangleShape(new Vector2f(16, 16));
            shape.Texture = texture;
            SetPos(position);
            shape.Scale = scale;
            shape.Origin = scale / 2;

            transform.position = shape.Position;
            transform.scale = shape.Scale;
            this.texture = texture;
            World.WorldUIObjects.Add(this);
        }        
    
        public void ToUpdate()
        {
            if (shape.GetGlobalBounds().Contains(Input.MousePos(false).X, Input.MousePos(false).Y))
            {
                if (Input.MousePressed())
                    OnClick?.Invoke();
    
                if (WasCursorButton == false)
                {
                    OnCursorEnter?.Invoke();
                    WasCursorButton = true;
                    WasCursorNonButton = false;
                }
            }
            else
            {
                if (WasCursorNonButton == false)
                {
                    OnCursorExit?.Invoke();
                    WasCursorNonButton = true;
                    WasCursorButton = false;
                }
            }
        }
    
        public void SetPos(Vector2f pos)
        {
            shape.Position = pos;
            transform.position = pos;
        }

        public void SetScale(Vector2f scale)
        {
            shape.Scale = scale;
            transform.scale = scale;
        }

        public void SetRot(float rot)
        {
            shape.Rotation = rot;
            transform.rotation = rot;
        }

        public void SetSize(Vector2f size)
        {
            shape.Size = size;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(shape, states);
        }

        public RectangleShape GetShape()
        {
            return shape;
        }

        public bool IsActive()
        {
            return active;
        }

        public List<Component> GetComponents()
        {
            throw new NotImplementedException();
        }
    }
}
