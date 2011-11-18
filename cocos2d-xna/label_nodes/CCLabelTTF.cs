/****************************************************************************
Copyright (c) 2010-2011 cocos2d-x.org
Copyright (c) 2008-2010 Ricardo Quesada
Copyright (c) 2011      Zynga Inc.
Copyright (c) 2011      Fulcrum Mobile Network, Inc.
 
http://www.cocos2d-x.org
http://www.openxlive.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace cocos2d
{
    /// <summary>
    /// CCLabelTTF is a subclass of CCTextureNode that knows how to render text labels
    /// All features from CCTextureNode are valid in CCLabelTTF
    /// CCLabelTTF objects are slow. Consider using CCLabelAtlas or CCLabelBMFont instead.
    /// </summary>
    public class CCLabelTTF : CCSprite, CCLabelProtocol
    {
        public CCLabelTTF()
        {

        }

        public string description()
        {
            string ret = string.Format("<CCLabelTTF | FontName = {0}, FontSize = {1}>", m_pFontName, m_fFontSize);
            return ret;
        }

        /// <summary>
        /// creates a CCLabelTTF from a fontname, alignment, dimension and font size
        /// </summary>
        public static CCLabelTTF labelWithString(string label, CCSize dimensions, CCTextAlignment alignment, string fontName, float fontSize)
        {
            CCLabelTTF pRet = new CCLabelTTF();
            if (pRet != null && pRet.initWithString(label, dimensions, alignment, fontName, fontSize))
            {
                //pRet->autorelease();
                return pRet;
            }
            //CC_SAFE_DELETE(pRet);
            return null;
        }

        /// <summary>
        /// creates a CCLabelTTF from a fontname and font size
        /// </summary>
        public static CCLabelTTF labelWithString(string label, string fontName, float fontSize)
        {
            CCLabelTTF pRet = new CCLabelTTF();
            if (pRet != null && pRet.initWithString(label, fontName, fontSize))
            {
                //pRet->autorelease();
                return pRet;
            }
            //CC_SAFE_DELETE(pRet);
            return null;
        }

        /// <summary>
        /// initializes the CCLabelTTF with a font name, alignment, dimension and font size
        /// </summary>
        private bool initWithString(string label, CCSize dimensions, CCTextAlignment alignment, string fontName, float fontSize)
        {
            Debug.Assert(label != null);
            if (init())
            {
                m_tDimensions = new CCSize(dimensions.width * CCDirector.sharedDirector().getFrames(), dimensions.height * CCDirector.sharedDirector().getFrames());
                m_eAlignment = alignment;

                if (m_pFontName != null)
                {
                    //delete m_pFontName;
                    m_pFontName = null;
                }
                m_pFontName = fontName;

                //m_fFontSize = fontSize * CC_CONTENT_SCALE_FACTOR();
                m_fFontSize = fontSize * CCDirector.sharedDirector().ContentScaleFactor;
                this.setString(label);
                return true;
            }
            return false;
        }

        /// <summary>
        /// initializes the CCLabelTTF with a font name and font size
        /// </summary>
        private bool initWithString(string label, string fontName, float fontSize)
        {
            Debug.Assert(label != null);
            if (init())
            {
                m_tDimensions = new CCSize(0, 0);

                if (m_pFontName != null)
                {
                    //delete m_pFontName;
                    m_pFontName = null;
                }
                m_pFontName = fontName;

                m_fFontSize = fontSize * CCDirector.sharedDirector().ContentScaleFactor;
                this.setString(label);
                return true;
            }
            return false;
        }

        /// <summary>
        /// changes the string to render
        /// @warning Changing the string is as expensive as creating a new CCLabelTTF. To obtain better performance use CCLabelAtlas
        /// </summary>
        public virtual void setString(string label)
        {
            if (m_pString != null)
            {
                //delete m_pString;
                m_pString = null;
            }
            m_pString = label;

            //CCTexture2D texture;
            //if (CCSize.CCSizeEqualToSize(m_tDimensions, new CCSize(0, 0)))
            //{
            //    texture = new CCTexture2D();
            //    texture.initWithString(label, m_pFontName.ToString(), m_fFontSize);
            //}
            //else
            //{
            //    texture = new CCTexture2D();
            //    texture.initWithString(label, m_tDimensions, m_eAlignment, m_pFontName.ToString(), m_fFontSize);
            //}
            //this.setTexture(texture);
            //texture->release();

            spriteFont = CCApplication.sharedApplication().content.Load<SpriteFont>("SpriteFont1");

            CCRect rect = new CCRect(0, 0, 0, 0);
            // rect.size = m_pobTexture.getContentSize();
            this.setTextureRect(rect);
        }

        public virtual string getString()
        {
            //return m_pString.c_str();
            return m_pString.ToString();
        }

        public override void draw()
        {
            CCApplication.sharedApplication().spriteBatch.Begin();
            CCApplication.sharedApplication().spriteBatch.DrawString(spriteFont, m_pString, new Vector2(position.x - anchorPointInPixels.x, position.y - anchorPointInPixels.y), Microsoft.Xna.Framework.Color.White);
            CCApplication.sharedApplication().spriteBatch.End();
        }

        //public virtual string gsString
        //{
        //    get
        //    {
        //        return m_pString.ToString();
        //    }
        //    set
        //    {
        //        if (m_pString != null)
        //        {
        //            //delete m_pString;
        //            m_pString = null;
        //        }
        //        m_pString = value;

        //        CCTexture2D texture;
        //        if (CCSize.CCSizeEqualToSize(m_tDimensions, new CCSize(0, 0)))
        //        {
        //            texture = new CCTexture2D();
        //            texture.initWithString(value, m_pFontName.ToString(), m_fFontSize);
        //        }
        //        else
        //        {
        //            texture = new CCTexture2D();
        //            texture.initWithString(value, m_tDimensions, m_eAlignment, m_pFontName.ToString(), m_fFontSize);
        //        }
        //        this.setTexture(texture);
        //        //texture->release();

        //        CCRect rect = new CCRect(0, 0, 0, 0);
        //        rect.size = m_pobTexture.getContentSize();
        //        this.setTextureRect(rect);
        //    }
        //}

        public virtual CCLabelProtocol convertToLabelProtocol()
        {
            return (CCLabelProtocol)this;
        }

        protected SpriteFont spriteFont;
        protected CCSize m_tDimensions;
        protected CCTextAlignment m_eAlignment;
        protected string m_pFontName;
        protected float m_fFontSize;
        protected string m_pString;
    }
}
